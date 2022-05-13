import { useState } from 'react';
import { Button } from '@mui/material';
import { ArrowBack, ArrowForward } from '@mui/icons-material';
import { addDays } from 'date-fns';
import { SelectDateRange, DateRange } from './Select';

export default function DateIntervalHeader(props: {updateDates: Function, startDate: Date, endDate: Date}) {
	const [dateRange, setDateRange] = useState(DateRange.Week);
	const interval = getDateInterval(dateRange);
	const [startDate, setStartDate] = useState(interval.start);
	const [endDate, setEndDate] = useState(interval.end);

	function onDateRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
		const range = parseInt(event.target.value) as DateRange;
		setDateRange(range);
		const interval = getDateInterval(range);
		setStartDate(interval.start);
		setEndDate(interval.end);
		props.updateDates(interval.start, interval.end);
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

	const intervalStr = startDate === endDate ? startDate.toLocaleDateString() : `${startDate.toLocaleDateString()} - ${endDate.toLocaleDateString()}`;
	return (
		<div>
			<SelectDateRange onChange={onDateRangeChange}/>
				<Button
					variant="contained"
					onClick={() => changePeriod(-1)}
					startIcon={<ArrowBack/>}
				/>
				<p style={{fontWeight: "bold"}}>{intervalStr}</p>
				<Button
					variant="contained"
					onClick={() => changePeriod(1)}
					startIcon={<ArrowForward/>}
				/>
		</div>
	);
}