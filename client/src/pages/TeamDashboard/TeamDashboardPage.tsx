import { useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Project, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { SelectProject, SelectDateRange } from '../../components/Select';
import TeamTimeChart from '../../components/Dashboard/TeamTimeChart';

export default function TeamDashboardPage() {
	const defaultDateRange = 7;
	const [dateEnd, setDateEnd] = useState(new Date(Date.now()));
	const [dateStart, setDateStart] = useState(addDays(dateEnd, -defaultDateRange + 1));

	const [projects, setProjects] = useState([] as Project[]);
	const [selectedProject, setSelectedProject] = useState(0);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		loadData()
		.then(
			(result) => {
				setProjects(result);
				setSelectedProject(result[0].id);
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	}, []);

	async function loadData() {
		return await fetchFunctionApi<Project[]>(`/Project`);
	}

	function onProjectChange(event: React.ChangeEvent<HTMLInputElement>) {
		setSelectedProject(parseInt(event.target.value));
	}

	function onDateRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
		const range = parseInt(event.target.value);
		setDateStart(addDays(dateEnd, -range + 1));
	}

	let mainUi: JSX.Element;

	if (state === State.Loading) {
		mainUi = <Typography>Loading...</Typography>
	}
	else if (state === State.Error) {
		mainUi = <Typography>Server unavailable</Typography>
	}
	else {
		const project = projects.find(p => p.id == selectedProject);

		mainUi = (
			<div className="mainbox">
				<h3>Статистика проекта {project.title}</h3>
				<div style={{display: "flex", alignItems: 'flex-start', position: 'relative', left: -100}}>
					<SelectProject projects={projects} onChange={onProjectChange}/>
					<div style={{paddingLeft: 30}}>
						<div style={{position: "relative", display: "flex", paddingBottom: 10}}>
							<p style={{fontWeight: "bold"}}>{dateStart.toLocaleDateString()} - {dateEnd.toLocaleDateString()}</p>
							<SelectDateRange onChange={onDateRangeChange}/>
						</div>
						<TeamTimeChart project={project} dateStart={dateStart} dateEnd={dateEnd}/>
					</div>
				</div>
			</div>
		)
	}

	return (
		<div>
			<Header/>
			{mainUi}
		</div>
	);
}