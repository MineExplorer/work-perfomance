import { useContext, useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Employee, State, TimeInterval } from '../../data';
import { fetchFunctionApi } from '../../helpers';
import { DateIntervalContext } from '../../stores/DateIntervalStore';
import { observer } from 'mobx-react';
import { DateRange, SelectProject } from '../Select';
import { makeStyles, Paper, Table, TableBody } from '@mui/material';
import { TableHead, TableRow, TableCell } from '@mui/material';

function TimeTable(props: {employee: Employee}) {
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
	}, [props.employee, startDate, endDate]);

	async function loadData() {
		return await fetchFunctionApi<TimeInterval[]>(`/TimeInterval/?employeeId=${props.employee.id}&dateStart=${store.getStartDateStr()}&dateEnd=${store.getEndDateStr()}`);
	}

	if (state === State.Loading) {
		return <Typography>Loading...</Typography>
	}
	else if (state === State.Error) {
		return <Typography>Server unavailable</Typography>
	}
	else {
		let sum = 0;
		timeRecords.forEach(val => sum += val.duration);

		const requiredTime = 8 * (dateRange == DateRange.Week ? 5 : dateRange == DateRange.Month ? 21 : 1);

		return (
			<Paper style={{maxWidth: 1000}}>
				<Table>
					<colgroup>
						<col style={{width:'25%'}}/>
						<col style={{width:'15%'}}/>
						<col style={{width:'10%'}}/>
						<col style={{width:'10%'}}/>
						<col style={{width:'40%'}}/>
					</colgroup>
					<TableHead>
						<TableRow>
							<TableCell style={{fontWeight: "bold"}}>Проект</TableCell>
							<TableCell style={{fontWeight: "bold"}}>Тип задачи</TableCell>
							{dateRange == DateRange.Day ? null : <TableCell style={{fontWeight: "bold"}}>Дата</TableCell>}
							<TableCell style={{fontWeight: "bold"}}>Время</TableCell>
							<TableCell style={{fontWeight: "bold"}}>Описание</TableCell>
						</TableRow>
					</TableHead>
					<TableBody>
						{timeRecords.map(row => {
							return <TableRow key={row.id}>
								<TableCell>{row.project}</TableCell>								
								<TableCell>{row.workType}</TableCell>
								{dateRange == DateRange.Day ? null : <TableCell>{row.date}</TableCell>}
								<TableCell>{row.duration + 'ч'}</TableCell>
								<TableCell>{row.description}</TableCell>
							</TableRow>
						})}
					</TableBody>
				</Table>
				<p>{`Время работы: ${sum} из ${requiredTime} часов`}</p>
			</Paper>
		)
	}
}

export default observer(TimeTable);