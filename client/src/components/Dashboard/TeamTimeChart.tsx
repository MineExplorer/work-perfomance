import {addDays, differenceInDays} from 'date-fns';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee, Project } from '../../data';
import { fetchFunctionApi } from '../../helpers';

const lineColors = ["#006eff", "green", "blue"];

export default function TeamTimeChart(props: {project: Project, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState([]);
    const [employees, setEmployees] = useState([]);

    useEffect(() => {
		loadTimeData()
		.then(
			(result) => {
				setTimeData(result);
			}
		)
		.catch(error => {
			console.log(error);
		});

		loadEmployeesData()
		.then(
			(result) => {
				setEmployees(result.employees);
			}
		)
		.catch(error => {
			console.log(error);
		});
	}, [props]);

    async function loadTimeData() {
        const dateStart = props.dateStart.toLocaleDateString();
        const dateEnd = props.dateEnd.toLocaleDateString();
        return await fetchFunctionApi<object[]>(`/TimeInterval/teamstats?projectId=${props.project.id}&dateStart=${dateStart}&dateEnd=${dateEnd}`);
	}

    async function loadEmployeesData() {
        return await fetchFunctionApi<Project>(`/Project/${props.project.id}`);
    }
    
    const totalTime = {};
    let totalTimeSum = 0;

	const statData = timeData.map((data, index) => {
        const chartData = {name: addDays(props.dateStart, index).toLocaleDateString()};
        for (let employee of employees) {
            const time = data[employee.id] ?? 0;
            chartData[employee.id] = time;
            totalTime[employee.id] = (totalTime[employee.id] || 0) + time;
            totalTimeSum += time;
        }
        return chartData;
    });

    console.log(statData);

    const renderLines = employees.map((employee, index) => {
        const lineName = employee.fullName;
        return <Line type="monotone" dataKey={employee.id} name={lineName} stroke={lineColors[index]} />
    });

    const renderText = employees.map((employee, index) => {
        return <p style={{color: lineColors[index]}}>{employee.fullName}: {totalTime[employee.id]} часов</p>
    });

    return (
        <div>
            <LineChart width={700} height={380} data={statData} margin={{
                top: 20,
                left: 20,
                right: 40
            }}>
                {renderLines}
                <CartesianGrid stroke="#ccc" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
            </LineChart>
            <div style={{marginLeft: 50, textAlign: "left"}}>
                <p>Итоговое время: {totalTimeSum} часов</p>
                {renderText}
            </div>
        </div>
    );
}