import { useContext, useEffect, useState } from 'react';
import { useLocalObservable } from 'mobx-react';
import { Button } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';

import { Employee, State } from '../../data';
import Header from '../../components/Header';
import { fetchFunctionApi } from '../../helpers';
import { AuthContext } from '../../stores/Auth';
import DateIntervalHeader from '../../components/DateIntervalHeader';
import { DateIntervalContext, DateIntervalStore } from '../../stores/DateIntervalStore';
import TimeTable from '../../components/TimeTable';
import { TimeTableContext, TimeTableStore } from '../../stores/TimeTableStore';

export default function TimeReportPage() {
	const authStore = useContext(AuthContext)
	const employeeId = authStore.userData.id;

	const [employeeData, setEmployeeData] = useState({} as Employee);
	const [state, setState] = useState(State.Loading);

	const intervalStore = useLocalObservable(() => new DateIntervalStore());
	const timeTableStore = useLocalObservable(() => new TimeTableStore());

	useEffect(() => {
		loadData()
		.then((result) => {
			setEmployeeData(result);
      timeTableStore.setEmployee(result);
			setState(State.Loaded);
		})
		.catch(error => {
			setState(State.Error);
		});
	}, []);

	async function loadData() {
		return await fetchFunctionApi<Employee>(`/Employee/${employeeId}`);
	}

	if (state === State.Loaded) {
		return (
			<div>
				<Header/>
				<DateIntervalContext.Provider value={intervalStore}>
          <TimeTableContext.Provider value={timeTableStore}>
            <div className="mainbox">
              <h3>Время работы сотрудника {employeeData.fullName}</h3>
              <div>
                <div style={{position: "relative"}}>
                  <DateIntervalHeader>
                    <Button onClick={() => timeTableStore.addRow()}>
                      <AddIcon/>
                    </Button>
                  </DateIntervalHeader>
                  <TimeTable employee={employeeData}/>
                </div>
              </div>
            </div>
          </TimeTableContext.Provider>
				</DateIntervalContext.Provider>
			</div>
		);
	}

	return (
		<div>
			<Header/>
		</div>
	)
}