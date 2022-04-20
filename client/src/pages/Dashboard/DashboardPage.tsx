import { useEffect, useState } from 'react';
import addDays from 'date-fns/addDays'
import Typography from '@mui/material/Typography';
import { Employee, State } from '../../data';
import Header from '../../components/Header';
import EmployeeTimeChart from '../../components/Dashboard/EmployeeTimeChart';
import { fetchFunctionApi } from '../../helpers';
import { SelectDateRange } from '../../components/Select';

export default function DashboardPage() {
	const employeeId = 1;
	const defaultDateRange = 7;
	const [dateEnd, setDateEnd] = useState(new Date(Date.now()));
	const [dateStart, setDateStart] = useState(addDays(dateEnd, -defaultDateRange + 1));

	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then(
			(result) => {
				setEmployeeData(result as Employee);
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
				<h3>Статистика сотрудника {employeeData.fullName}</h3>
				<div>
					<div style={{position: "relative"}}>
						<div style={{position: "relative", display: "flex", padding: 10}}>
							<p style={{fontWeight: "bold"}}>{dateStart.toLocaleDateString()} - {dateEnd.toLocaleDateString()}</p>
							<SelectDateRange onChange={onDateRangeChange}/>
						</div>
						<EmployeeTimeChart employee={employeeData} dateStart={dateStart} dateEnd={dateEnd}/>
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