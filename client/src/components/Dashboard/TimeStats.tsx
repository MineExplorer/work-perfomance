import {addDays, differenceInDays} from 'date-fns';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee } from '../../data';
import { fetchFunctionApi } from '../../helpers';

export default function TimeStats(props: {employee: Employee, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState([]);

    useEffect(() => {
		loadData()
		.then(
			(result) => {
				setTimeData(result);
                console.log(result);
			}
		)
		.catch(error => {
			console.log(error);
		});
	}, [props]);

    async function loadData() {
        const timeData = [];
        for (var project of props.employee.projects) {
            const dateStart = props.dateStart.toLocaleDateString();
            const dateEnd = props.dateEnd.toLocaleDateString();
		    timeData.push(await fetchFunctionApi<number[]>(`/TimeInterval/stats?employeeId=${props.employee.id}&projectId=${project.id}&dateStart=${dateStart}&dateEnd=${dateEnd}`));
        }
        return timeData;
	}
    
    let totalTime = 0;
	let daysWorked = 0;

	const statData = [];
    if (timeData.length > 0) {
        const dateRange = timeData[0].length;
        for (let i = 0; i < dateRange; i++) {
            let dayData = {name: addDays(props.dateStart, i).toLocaleDateString()};
            let worked = false;
            for (let j = 0; j < timeData.length; j++) {
                const timeHours = timeData[j][i];
                dayData['h' + j] = timeHours;
                if (timeHours > 0) {
                    totalTime += timeHours;
                    worked = true;
                }
            }
            statData.push(dayData);
            if (worked) {
                daysWorked++;
            }
        }
    }
    console.log(statData);

    const renderLines = timeData.map((data, index) => {
        return <Line type="monotone" dataKey={'h' + index} stroke="#006eff" />
    });

    return (
        <div>
            <LineChart width={700} height={380} data={statData} margin={{
                top: 20,
                left: 20,
                right: 20
            }}>
                {renderLines}
                <CartesianGrid stroke="#ccc" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
            </LineChart>
            <p>Итоговое время: {totalTime} часов</p>
            <p>Среднее в день: {+(totalTime / (daysWorked || 1)).toPrecision(2)} часов</p>
        </div>
    );
}