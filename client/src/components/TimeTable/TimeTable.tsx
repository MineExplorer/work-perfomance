import { useContext, useEffect, useState } from 'react';
import { observer } from 'mobx-react';
import { Button, Paper, Table, TableBody } from '@mui/material';
import { TableHead, TableRow, TableCell } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import EditIcon from '@mui/icons-material/Edit';
import SaveIcon from '@mui/icons-material/Save';

import Typography from '@mui/material/Typography';
import { Employee, State, TimeInterval } from '../../data';
import { fetchFunctionApi, deleteFunctionApi } from '../../helpers';
import { DateIntervalContext } from '../../stores/DateIntervalStore';
import { DateRange, SelectProject, SelectTime, SelectWorkType } from '../Select';

function TimeTable(props: {employee: Employee}) {
  const store = useContext(DateIntervalContext);
  const {dateRange, startDate, endDate} = store;

  const [timeRecords, setTimeRecords] = useState([] as TimeInterval[]);
  const [editingRow, setEditingRow] = useState(-1);
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

  async function deleteRow(i: number, row: TimeInterval) {
    const list = [...timeRecords];
    list.splice(i, 1);
    setTimeRecords(list);
    setEditingRow(-1);
    if (row.id) {
      await deleteFunctionApi(`/TimeInterval/${row.id}`);
    }
  }

  function addRow(i: number) {
    const list = [...timeRecords];
    const project = props.employee.projects[0];
    //@ts-ignore
    list.splice(i + 1, 0, {
      employeeId: props.employee.id,
      projectId: project.id,
      project: project.title,
      workTypeId: 1,
      workType: "Разработка",
      date: "",
      duration: 0.25,
      description: ""
    });
    setTimeRecords(list);
    setEditingRow(i + 1);
  }

  function saveRow(i: number) {
    setTimeRecords(timeRecords);
    setEditingRow(-1);
  }

  function handleInputChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
    const { name, value } = e.target;
    const list = [...timeRecords];
    list[index][name] = value;
    setTimeRecords(list);
  };

  function handleSelectIdChange(e: React.ChangeEvent<HTMLSelectElement>, index: number) {
    const name = e.target.name;
    const value = parseInt(e.target.value);
    const list = [...timeRecords];
    const text = e.target.options[value - 1].text;
    list[index][name + "Id"] = value;
    list[index][name] = text;
    setTimeRecords(list);
  };

  function handleSelectChange(e: React.ChangeEvent<HTMLSelectElement>, index: number) {
    const name = e.target.name;
    const list = [...timeRecords];
    list[index][name] = parseFloat(e.target.value);
    setTimeRecords(list);
  };

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
              <TableCell style={{fontWeight: "bold"}}>Дата</TableCell>
              <TableCell style={{fontWeight: "bold"}}>Время</TableCell>
              <TableCell style={{fontWeight: "bold"}}>Описание</TableCell>
              <TableCell/>
              <TableCell/>
            </TableRow>
          </TableHead>
          <TableBody>
            {timeRecords.map((row, i) => {
              return i == editingRow ? (
                <TableRow key={row.id}>
                  <TableCell>
                    <SelectProject
                      projects={props.employee.projects}
                      value={row.projectId}
                      onChange={(e) => handleSelectIdChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <SelectWorkType
                      value={row.workTypeId}
                      onChange={(e) => handleSelectIdChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <input
                      name="date"
                      style={{maxWidth: 70}}
                      value={row.date}
                      size={10}
                      onChange={(e) => handleInputChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <SelectTime
                      value={row.duration} 
                      onChange={(e) => handleSelectChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <input
                      name="description"
                      value={row.description}
                      size={36}
                      onChange={(e) => handleInputChange(e, i)}
                    />
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => saveRow(i)}>
                      <SaveIcon/>
                    </Button>
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => deleteRow(i, row)}>
                      <DeleteOutlineIcon/>
                    </Button>
                  </TableCell>
                </TableRow>
              ) : (
                <TableRow key={row.id}>
                  <TableCell>{row.project}</TableCell>								
                  <TableCell>{row.workType}</TableCell>
                  <TableCell>{row.date}</TableCell>
                  <TableCell>{row.duration + 'ч'}</TableCell>
                  <TableCell>{row.description}</TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => setEditingRow(i)}>
                      <EditIcon/>
                    </Button>
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => addRow(i)}>
                      <AddIcon/>
                    </Button>
                  </TableCell>
                </TableRow>
              )
            })}
          </TableBody>
        </Table>
        <p>{`Время работы: ${sum} из ${requiredTime} часов`}</p>
      </Paper>
    )
  }
}

export default observer(TimeTable);