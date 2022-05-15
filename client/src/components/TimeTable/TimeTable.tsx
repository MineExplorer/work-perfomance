import { useContext, useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Employee, State, TimeInterval } from '../../data';
import { fetchFunctionApi } from '../../helpers';
import { DateIntervalContext } from '../../stores/DateIntervalStore';
import { observer } from 'mobx-react';
import { DateRange } from '../Select';

function TimeTable(props: {employeeId: number}) {
	const store = useContext(DateIntervalContext);
	const {dateRange, startDate, endDate} = store;

	const [timeRecords, setTimeRecords] = useState([] as TimeInterval[]);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		loadData()
		.then(
			(result) => {
				setTimeRecords(result);
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	}, [props.employeeId, startDate, endDate]);

	async function loadData() {
		return await fetchFunctionApi<TimeInterval[]>(`/TimeInterval/?employeeId=${props.employeeId}&dateStart=${store.getStartDateStr()}&dateEnd=${store.getEndDateStr()}`);
	}

	if (state === State.Loading) {
		return <Typography>Loading...</Typography>
	}
	else if (state === State.Error) {
		return <Typography>Server unavailable</Typography>
	}
	else {
		return (
			<div className="mainbox">
				<table>
					<tr>
						<th>Проект</th>
						<th>Тип задачи</th>
						{dateRange == DateRange.Day ? null : <th>Дата</th>}
						<th>Время</th>
						<th>Описание</th>
					</tr>
					{timeRecords.map(value => {
						return <tr>
							<td>{value.project}</td>
							<td>{value.workType}</td>
							{dateRange == DateRange.Day ? null : <td>{value.date}</td>}
							<td>{value.duration + 'ч'}</td>
							<td>{value.description}</td>
						</tr>
					})}
				</table>
			</div>
		)
	}
}

export default observer(TimeTable);