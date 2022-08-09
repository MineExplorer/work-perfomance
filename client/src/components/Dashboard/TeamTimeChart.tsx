import {addDays, differenceInDays} from 'date-fns';
import { useEffect, useState } from 'react';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee, Project } from '../../data';
import { fetchFunctionApi, lineColors } from '../../helpers';

type NumberArrayMap = {[key: number]: number[]};

export default function TeamTimeChart(props: {project: Project, dateStart: Date, dateEnd: Date}) {
    const [timeData, setTimeData] = useState({} as NumberArrayMap);
    const [employees, setEmployees] = useState([] as Employee[]);

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

		loadProjectData()
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
        return await fetchFunctionApi<NumberArrayMap>(`/TimeInterval/teamstats?projectId=${props.project.id}&dateStart=${dateStart}&dateEnd=${dateEnd}`);
	}

    async function loadProjectData() {
        return await fetchFunctionApi<Project>(`/Project/${props.project.id}`);
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
    for (let employee of employees) {
        const index = renderLines.length;
        const color = lineColors[index];
        const lineName = employee.fullName;
        renderLines.push(<Line type="monotone" key={employee.id} name={lineName} dataKey={employee.id} stroke={color} />);
        
        const totalTime = timeData[employee.id]?.reduce((sum, value) => sum += value);
        totalTimeSum += totalTime;
        
        renderText.push(<p key={employee.id} style={{color: color}}>{lineName}: {totalTime} часов</p>);
    }

    return (
        <div>
            <LineChart width={700} height={380} data={statData} margin={{
                top: 20,
                left: 20,
                right: 40
            }}>
                <CartesianGrid stroke="#ccc" />
                {renderLines}
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