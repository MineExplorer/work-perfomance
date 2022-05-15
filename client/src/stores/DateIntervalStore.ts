import { addDays } from 'date-fns';
import {action, makeObservable, observable} from 'mobx';
import { createContext } from 'react';
import { DateRange } from '../components/Select/SelectDateRange';

export class DateIntervalStore {
	@observable
	dateRange = DateRange.Week

	@observable
	startDate: Date;

	@observable
	endDate: Date;

	constructor() {
		makeObservable(this);
		this.setInterval(this.dateRange);
	}

	getStartDateStr = () => {
		return this.startDate.toLocaleDateString();
	}

	getEndDateStr = () => {
		return this.endDate.toLocaleDateString();
	}

	@action
	onDateRangeChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		const range = parseInt(event.target.value) as DateRange;
		this.dateRange = range;
		this.setInterval(range);
	}

	@action
	changePeriod = (direction: number) => {
		const previousDate = this.changeDate(this.startDate, direction);
		this.setInterval(this.dateRange, previousDate);
	}

	@action
	private setInterval(range: DateRange, currentDate: Date = new Date(Date.now())) {
		const interval = this.getDateInterval(range, currentDate);
		this.startDate = interval.start;
		this.endDate = interval.end;
	}

	@action
	private getDateInterval(range: DateRange, currentDate: Date): {start: Date, end: Date} {
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

	@action
	private changeDate(date: Date, direction: number): Date {
		switch(this.dateRange) {
			case DateRange.Day:
				return addDays(date, direction);
			case DateRange.Week:
				return addDays(date, direction * 7);
			case DateRange.Month:
				const newDate = date.setMonth(date.getMonth() + direction);
				return new Date(newDate);
		}
	}
}

export const DateIntervalContext = createContext<DateIntervalStore>(null);