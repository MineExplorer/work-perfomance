import addDays from 'date-fns/addDays';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { fetchFunctionApi } from '../../helpers';

export default function TimeStats(props: {employeeId: number, projectId: number, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState([]);

    useEffect(() => {
		loadData()
		.then(
			(result) => {
				setTimeData(result);
			}
		)
		.catch(error => {
			console.log(error);
		});
	}, [props]);

    async function loadData() {
		return await fetchFunctionApi<number[]>(`/TimeInterval/stats?employeeId=${props.employeeId}&projectId=${props.projectId}&dateStart=${props.dateStart.toLocaleDateString()}&dateEnd=${props.dateEnd.toLocaleDateString()}`);
	}
    
    let totalTime = 0;
	let dayWorked = 0;

	const statData = [];
	for (let i = 0; i < timeData.length; i++) {
		const timeHours = timeData[i];
		statData.push({name: addDays(props.dateStart, i).toLocaleDateString(), hours: timeHours});
		if (timeHours > 0) {
			totalTime += timeHours;
			dayWorked++;
		}
	}

    return (
        <div>
            <LineChart width={700} height={380} data={statData} margin={{
                top: 20,
                left: 20,
                right: 20
            }}>
                <Line type="monotone" dataKey="hours" stroke="#006eff" />
                <CartesianGrid stroke="#ccc" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
            </LineChart>
            <p>Итоговое время: {totalTime} часов</p>
            <p>Среднее в день: {+(totalTime / (dayWorked || 1)).toPrecision(2)} часов</p>
        </div>
    );
}