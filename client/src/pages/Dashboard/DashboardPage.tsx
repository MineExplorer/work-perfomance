import { useEffect, useState } from 'react';
import { Box } from 'grommet';
import MaterialUIBox from '@mui/material/Box';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, TimeInterval, State } from '../../data';
import Header from '../../components/Header';
import TimeStats from '../../components/Dashboard/TimeStats';
import { fetchFunctionApi } from '../../helpers';
import { SelectProject, SelectDateRange } from '../../components/Select';

export default function DashboardPage() {
	const employeeId = 1;
	const defaultDateRange = 7;
	const [dateEnd, setDateEnd] = useState(new Date(Date.now()));
	const [dateStart, setDateStart] = useState(addDays(dateEnd, -defaultDateRange + 1));
	const [selectedProject, setSelectedProject] = useState(0);

	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				setEmployeeData(result as Employee);
				if (result.projects.length > 0) {
					setSelectedProject(result.projects[0].id);
				}
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	});

	async function loadData() {
		return await fetchFunctionApi<Employee>(`/Employee/${employeeId}`);
	}


	if (state === State.Loading) {
		return <Typography>Loading...</Typography>
	}
	
	if (state === State.Error) {
		return <Typography>Server unavailable</Typography>
	}

	function onProjectChange(event: React.ChangeEvent<HTMLInputElement>) {
		setSelectedProject(parseInt(event.target.value));
	}

	function onDateRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
		const range = parseInt(event.target.value);
		setDateStart(addDays(dateEnd, -range + 1));
	}

	return (
		<div>
			<Header/>
			<div className="mainbox">
				<h3>Статистика сотрудника {employeeData.fullName}</h3>
				<div>
					<div style={{position: "relative"}}>
						<div style={{position: "relative", display: "flex", padding: 10}}>
							<p style={{fontWeight: "bold"}}>{dateStart.toLocaleDateString()} - {dateEnd.toLocaleDateString()}</p>
							<SelectProject projects={employeeData.projects} onChange={onProjectChange}/>
							<SelectDateRange onChange={onDateRangeChange}/>
						</div>
						<TimeStats employee={employeeData} dateStart={dateStart} dateEnd={dateEnd}/>
					</div>
				</div>
			</div>
		</div>
	);
}