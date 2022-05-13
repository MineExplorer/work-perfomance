import { useContext, useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { SelectDateRange, DateRange } from '../../components/Select';
import { Button } from '@mui/material';
import { ArrowBack, ArrowForward } from '@mui/icons-material';
import { AuthContext } from '../../stores/Auth';

export default function TimeReportPage() {
	const store = useContext(AuthContext)
	const employeeId = store.userData.id;

	const [dateRange, setDateRange] = useState(DateRange.Week);
	const interval = getDateInterval(dateRange);
	const [startDate, setStartDate] = useState(interval.start);
	const [endDate, setEndDate] = useState(interval.end);

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
		mainUi = (
			<div className="mainbox">
				<h3>Время работы сотрудника {employeeData.fullName}</h3>
				<div>
					<div style={{position: "relative"}}>
						<div style={{position: "relative", display: "flex", padding: 10}}>
							<SelectDateRange onChange={onDateRangeChange}/>
							<Button
								variant="contained"
								onClick={() => changePeriod(-1)}
								startIcon={<ArrowBack/>}
							/>
							<p style={{fontWeight: "bold"}}>{startDate === endDate ? startDate.toLocaleDateString() : `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`}</p>
							<Button
								variant="contained"
								onClick={() => changePeriod(1)}
								startIcon={<ArrowForward/>}
							/>
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