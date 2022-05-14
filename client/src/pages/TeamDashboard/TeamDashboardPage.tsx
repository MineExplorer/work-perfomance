import { useContext, useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Project, State } from '../../data';
import Header from '../../components/Header';
import DateIntervalHeader from '../../components/DateIntervalHeader';
import { fetchFunctionApi } from '../../helpers';
import { SelectProject } from '../../components/Select';
import TeamTimeChart from '../../components/Dashboard/TeamTimeChart';
import Loading from '../../components/Loading';
import { DateIntervalStore } from '../../stores/DateIntervalStore';
import { observer, useLocalObservable } from 'mobx-react';
import { DateIntervalContext } from '../App/AppContainer';

export default observer(() => {
	const [projects, setProjects] = useState([] as Project[]);
	const [selectedProject, setSelectedProject] = useState(0);
	const [state, setState] = useState(State.Loading);

	const intervalStore = useContext(DateIntervalContext);

	useEffect(() => {
		loadData()
		.then((result) => {
			setProjects(result);
			setSelectedProject(result[0].id);
			setState(State.Loaded);
		})
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

	let mainUi: JSX.Element;

	if (state === State.Loading) {
		mainUi = <Loading/>
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
						<DateIntervalHeader/>
						<TeamTimeChart project={project} dateStart={intervalStore.startDate} dateEnd={intervalStore.endDate}/>
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
});