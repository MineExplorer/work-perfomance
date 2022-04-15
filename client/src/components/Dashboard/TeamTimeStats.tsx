import {addDays, differenceInDays} from 'date-fns';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee, Project } from '../../data';
import { fetchFunctionApi } from '../../helpers';

export default function TimeStats(props: {project: Project, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState([]);
    const employees = props.project.employees;
    console.log(employees);

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
        const dateStart = props.dateStart.toLocaleDateString();
        const dateEnd = props.dateEnd.toLocaleDateString();
        return await fetchFunctionApi<object[]>(`/TimeInterval/teamstats?projectId=${props.project.id}&dateStart=${dateStart}&dateEnd=${dateEnd}`);
	}
    
    let totalTime = 0;
	let daysWorked = 0;

	const statData = timeData.map((data, index) => {
        const chartData = {name: addDays(props.dateStart, index).toLocaleDateString()};
        for (let employee of employees) {
            chartData[employee.id] = data[employee.id] ?? 0;
        }
        return chartData;
    });

    console.log(statData);

    const renderLines = employees.map(employee => {
        const lineName = employee.fullName;
        return <Line type="monotone" dataKey={employee.id} name={lineName} stroke="#006eff" />
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