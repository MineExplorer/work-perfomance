import { useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Project, State } from '../../data';
import Header from '../../components/Header';
import DateIntervalHeader from '../../components/DateIntervalHeader';
import { fetchFunctionApi } from '../../helpers';
import { SelectProject, SelectDateRange, DateRange } from '../../components/Select';
import TeamTimeChart from '../../components/Dashboard/TeamTimeChart';

export default function TeamDashboardPage() {
	const [dateRange, setDateRange] = useState(DateRange.Week);
	const interval = getDateInterval(dateRange);
	const [startDate, setStartDate] = useState(interval.start);
	const [endDate, setEndDate] = useState(interval.end);

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
		const range = parseInt(event.target.value) as DateRange;
		setDateRange(range);
		const interval = getDateInterval(range);
		setStartDate(interval.start);
		setEndDate(interval.end);
	}

	function getDateInterval(range: DateRange, currentDate: Date = new Date(Date.now())): {start: Date, end: Date} {
		switch(range) {
			case DateRange.Day:
				return {start: currentDate, end: currentDate};
			case DateRange.Week:
				let day = currentDate.getDay();
				day = day === 0 ? 6 : day - 1;
				const startDate = addDays(currentDate, -day);
				return {
					start: startDate,
					end: addDays(startDate, 6)
				}
			case DateRange.Month:
				return {
					start: new Date(currentDate.setDate(1)),
					end: new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0)
				}
		}
	}

	function changeDate(date: Date, direction: number): Date {
		switch(dateRange) {
			case DateRange.Day:
				return addDays(date, direction);
			case DateRange.Week:
				return addDays(date, direction * 7);
			case DateRange.Month:
				const newDate = date.setMonth(date.getMonth() + direction);
				return new Date(newDate);
		}
	}

	function changePeriod(direction: number) {
		const previousDate = changeDate(startDate, direction);
		const interval = getDateInterval(dateRange, previousDate);
		setStartDate(interval.start);
		setEndDate(interval.end);
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
							<DateIntervalHeader
								startDate={startDate}
								endDate={endDate}
								updateDates={() => void 0}
							/>
						</div>
						<TeamTimeChart project={project} dateStart={startDate} dateEnd={endDate}/>
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