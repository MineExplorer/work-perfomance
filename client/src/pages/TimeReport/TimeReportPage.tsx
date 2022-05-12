import { useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, State, TimeInterval } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { SelectTimeRange, TimeRange } from '../../components/Select';

export default function TimeReportPage() {
	const employeeId = 1;

	const interval = getDateInterval(TimeRange.Week);
	const [dateStart, setDateStart] = useState(interval.start);
	const [dateEnd, setDateEnd] = useState(interval.end);

	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				setEmployeeData(result);
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

	function onTimeRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
		const range = parseInt(event.target.value) as TimeRange;
		const interval = getDateInterval(range);
		setDateStart(interval.start);
		setDateEnd(interval.end);
	}

	function getDateInterval(timeRange: TimeRange): {start: Date, end: Date} {
		const today = new Date(Date.now());
		switch(timeRange) {
			case TimeRange.Day:
				return {start: today, end: today};
			case TimeRange.Week:
				const day = today.getDay();
				const startDate = addDays(today, -day + 1);
				return {
					start: startDate,
					end: addDays(startDate, 6)
				}
			case TimeRange.Month:
				return {
					start: new Date(today.setDate(1)),
					end: new Date(today.getFullYear(), today.getMonth() + 1, 0)
				}
		}
	}

	let mainUi: JSX.Element;

	if (state === State.Loading) {
		mainUi = <Typography>Loading...</Typography>
	}
	else if (state === State.Error) {
		mainUi = <Typography>Server unavailable</Typography>
	}
	else {
		mainUi = (
			<div className="mainbox">
				<h3>Время работы сотрудника {employeeData.fullName}</h3>
				<div>
					<div style={{position: "relative"}}>
						<div style={{position: "relative", display: "flex", padding: 10}}>
							<p style={{fontWeight: "bold"}}>{dateStart.toLocaleDateString()} - {dateEnd.toLocaleDateString()}</p>
							<SelectTimeRange onChange={onTimeRangeChange}/>
						</div>
						
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