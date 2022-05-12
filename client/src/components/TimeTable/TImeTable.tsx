import { useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, State, TimeInterval } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';

export default function TimeReportPage(props: {employeeId: number, dateStart: Date, dateEnd: Date}) {
	const defaultDateRange = 7;
	const [dateEnd, setDateEnd] = useState(new Date(Date.now()));
	const [dateStart, setDateStart] = useState(addDays(dateEnd, -defaultDateRange + 1));

	const [timeData, setTimeData] = useState([] as TimeInterval[]);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				setTimeData(result);
				setState(State.Loaded);
			}
		)
		.catch(error => {
			setState(State.Error);
		});
	});

	async function loadData() {
		return await fetchFunctionApi<TimeInterval[]>(`/TimeInterval/?employeeId=${props.employeeId}`);
	}

	function onTimeRangeChange(event: React.ChangeEvent<HTMLInputElement>) {
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
		mainUi = (
			<div className="mainbox">
				
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