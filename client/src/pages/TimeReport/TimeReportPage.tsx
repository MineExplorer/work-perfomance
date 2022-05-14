import { useContext, useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { Employee, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { AuthContext } from '../../stores/Auth';
import Loading from '../../components/Loading';
import { useLocalObservable } from 'mobx-react';
import { DateIntervalStore } from '../../stores/DateIntervalStore';
import DateIntervalHeader from '../../components/DateIntervalHeader';

export default function TimeReportPage() {
	const authStore = useContext(AuthContext)
	const employeeId = authStore.userData.id;

	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	useEffect(() => {
		if (state === State.Loaded) return;

		loadData()
		.then((result) => {
			setEmployeeData(result);
			setState(State.Loaded);
		})
		.catch(error => {
			setState(State.Error);
		});
	});

	async function loadData() {
		return await fetchFunctionApi<Employee>(`/Employee/${employeeId}`);
	}

	let mainUi: JSX.Element;

	if (state === State.Loading) {
		mainUi = <Loading/>
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
						<DateIntervalHeader/>
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