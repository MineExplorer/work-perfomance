import {useContext, useEffect, useState } from 'react';
import Typography from '@mui/material/Typography';
import { observer, useLocalObservable } from 'mobx-react';

import { Employee, State } from '../../data';
import Header from '../../components/Header';
import EmployeeTimeChart from '../../components/Dashboard/EmployeeTimeChart';
import { fetchFunctionApi } from '../../helpers';
import { AuthContext } from '../../stores/Auth';
import Loading from '../../components/Loading';
import { DateIntervalContext, DateIntervalStore } from '../../stores/DateIntervalStore';
import DateIntervalHeader from '../../components/DateIntervalHeader';
import TimeStat from '../../components/Dashboard/TimeStat';

const DashboardPage: React.FC = () => {
	const store = useContext(AuthContext)
	const employeeId = store.userData.id;
	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	const intervalStore = useLocalObservable(() => new DateIntervalStore());

	useEffect(() => {
		loadData()
		.then((result) => {
			setEmployeeData(result);
			setState(State.Loaded);
		})
		.catch(error => {
			setState(State.Error);
		});
	}, []);

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
				<h3>Статистика сотрудника {employeeData.fullName}</h3>
				<div>
					<div style={{position: "relative"}}>
						<DateIntervalHeader/>
						<EmployeeTimeChart employee={employeeData} dateStart={intervalStore.startDate} dateEnd={intervalStore.endDate}/>
						<TimeStat employeeId={employeeId}/>
					</div>
				</div>
			</div>
		)
	}

	return (
		<div>
			<Header/>
			<DateIntervalContext.Provider value={intervalStore}>
				{mainUi}
			</DateIntervalContext.Provider>
		</div>
	);
}

export default observer(DashboardPage)