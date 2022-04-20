import {addDays, differenceInDays} from 'date-fns';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee, Project } from '../../data';
import { fetchFunctionApi, lineColors } from '../../helpers';

type NumberArrayMap = {[key: number]: number[]};

export default function EmployeeTimeChart(props: {employee: Employee, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState({} as NumberArrayMap);
    const [projects, setProjects] = useState([] as Project[]);

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

		loadEmployeeData()
		.then(
			(result) => {
				setProjects(result.projects);
			}
		)
		.catch(error => {
			console.log(error);
		});
	}, [props]);

    async function loadTimeData() {
        const dateStart = props.dateStart.toLocaleDateString();
        const dateEnd = props.dateEnd.toLocaleDateString();
        return await fetchFunctionApi<NumberArrayMap>(`/TimeInterval/stats?employeeId=${props.employee.id}&dateStart=${dateStart}&dateEnd=${dateEnd}`);
	}

    async function loadEmployeeData() {
        return await fetchFunctionApi<Employee>(`/Employee/${props.employee.id}`);
    }
    
	const statData = [];
    const dateRange = differenceInDays(props.dateEnd, props.dateStart) + 1;
    for (let i = 0; i < dateRange; i++) {
        let dayData = {name: addDays(props.dateStart, i).toLocaleDateString()};
        for (let id in timeData) {
            const timeHours = timeData[id][i];
            dayData[id] = timeHours;
        }
        statData.push(dayData);
    }

    const renderLines = [];
    const renderText = [];
    let totalTimeSum = 0;
    for (let id in timeData) {
        const index = renderLines.length;
        const color = lineColors[index];
        const lineName = projects.find(p => p.id == parseInt(id)).title;
        renderLines.push(<Line type="monotone" name={lineName} dataKey={id} stroke={color} />);
        
        const totalTime = timeData[id].reduce((sum, value) => sum += value);
        totalTimeSum += totalTime;
        
        renderText.push(<p style={{color: color}}>{lineName}: {totalTime} часов</p>);
    }

    return (
        <div>
            <LineChart width={700} height={380} data={statData} margin={{
                top: 20,
                left: 20,
                right: 20
            }}>
                <CartesianGrid stroke="#ccc" />
                {renderLines}
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
            </LineChart>
            <div style={{marginLeft: 50, textAlign: "left"}}>
                <p>Итоговое время: {totalTimeSum} часов</p>
                <p>Среднее в день: {+(totalTimeSum / (dateRange || 1)).toPrecision(2)} часов</p>
                {renderText}
            </div>
        </div>
    );
}