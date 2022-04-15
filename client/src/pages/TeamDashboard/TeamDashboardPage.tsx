import { useEffect, useState } from 'react';
import { Box } from 'grommet';
import MaterialUIBox from '@mui/material/Box';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, Project, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { SelectProject, SelectDateRange } from '../../components/Select';
import TeamTimeStats from '../../components/Dashboard/TeamTimeStats';

export default function TeamDashboardPage() {
	const projectId = 2;
	const defaultDateRange = 7;
	const [dateEnd, setDateEnd] = useState(new Date(Date.now()));
	const [dateStart, setDateStart] = useState(addDays(dateEnd, -defaultDateRange + 1));

	const [projectData, seProjectData] = useState({} as Project);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				seProjectData(result);
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	});

	async function loadData() {
		return await fetchFunctionApi<Project>(`/Project/${projectId}`);
	}


	if (state === State.Loading) {
		return <Typography>Loading...</Typography>
	}
	
	if (state === State.Error) {
		return <Typography>Server unavailable</Typography>
	}

	function onDateRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
		const range = parseInt(event.target.value);
		setDateStart(addDays(dateEnd, -range + 1));
	}

	return (
		<div>
			<Header/>
			<div className="mainbox">
				<h3>Статистика проекта {projectData.title}</h3>
				<div>
					<div style={{position: "relative"}}>
						<div style={{position: "relative", display: "flex", padding: 10}}>
							<p style={{fontWeight: "bold"}}>{dateStart.toLocaleDateString()} - {dateEnd.toLocaleDateString()}</p>
							<SelectDateRange onChange={onDateRangeChange}/>
						</div>
						<TeamTimeStats project={projectData} dateStart={dateStart} dateEnd={dateEnd}/>
					</div>
				</div>
			</div>
		</div>
	);
}