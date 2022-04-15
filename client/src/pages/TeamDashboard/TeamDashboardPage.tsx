import { useEffect, useState } from 'react';
import { Box } from 'grommet';
import MaterialUIBox from '@mui/material/Box';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { LineChart, Line, CartesianGrid, XAxis, YAxis, Tooltip } from 'recharts';
import { Employee, TimeInterval, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';

export default function TeamDashboardPage() {
	const employeeId = 1;
	const dateRange = 7;
	const dateEnd = new Date(Date.now());
	const dateStart = addDays(dateEnd, -dateRange);
	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [data, setData] = useState([]);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				setEmployeeData(result[0] as Employee);
				setData(result[1] as number[]);
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	});

	const loadData = async () => {
		return [
			await fetchFunctionApi<Employee>(`/Employee/${employeeId}`),
			await fetchFunctionApi<number[]>(`/TimeInterval/stats?employeeId=${employeeId}&projectId=1&dateStart=${dateStart.toLocaleDateString()}&dateEnd=${dateEnd.toLocaleDateString()}`)
		]
	}

	if (state === State.Loading) {
		return <Typography>Loading...</Typography>
	}
	
	if (state === State.Error) {
		return <Typography>Server unavailable</Typography>
	}

	let projects = [];

	let projectId = 0;
	
	let totalTime = 0;
	let dayWorked = 0;

	function generateOptions(): JSX.Element[] {
		const renderData = [];
		for (let project of projects) {
			renderData.push(<option id={project.id.toString()}>{project.title}</option>);
		}
		return renderData;
	}

	const statData = [];
	for (let i = 0; i < data.length; i++) {
		const timeHours = data[i];
		statData.push({name: addDays(dateStart, i).toLocaleDateString(), hours: timeHours});
		if (timeHours > 0) {
			totalTime += timeHours;
			dayWorked++;
		}
	}

	const renderLineChart = (
		<LineChart width={700} height={380} data={statData} margin={{
			top: 20,
			left: 20,
			right: 20,
		}}>
			<Line type="monotone" dataKey="hours" stroke="#006eff" />
			<CartesianGrid stroke="#ccc" />
			<XAxis dataKey="name" />
			<YAxis />
			<Tooltip />
		</LineChart>
	);

	return (
		<div>
			<Header/>
			<div className="mainbox">
				<h3>Статистика сотрудника {employeeData.fullName}</h3>
				<div>
					<select className="selectMenu">
						{generateOptions()}
					</select>
					<div style={{position: "relative", top: -40}}>
						{renderLineChart}
						<p>Итоговое время: {totalTime} часов</p>
						<p>Среднее в день: {totalTime / (dayWorked || 1)} часов</p>
					</div>
				</div>
			</div>
		</div>
	);
}