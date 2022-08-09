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
import { DateIntervalContext } from '../../stores/DateIntervalStore';
import { DateRange, SelectProject, SelectTime, SelectWorkType } from '../Select';
import { TimeTableContext } from '../../stores/TimeTableStore';

function TimeTable(props: {employee: Employee}) {
  const dateStore = useContext(DateIntervalContext);
  const {dateRange, startDate, endDate} = dateStore;

  const timeRecordsStore = useContext(TimeTableContext);
  const {timeRecords, editingRow, state} = timeRecordsStore;

  useEffect(() => {
    loadData();
  }, [props.employee, startDate, endDate]);

  async function loadData() {
    await timeRecordsStore.fetchTimeIntervals(dateStore.getStartDateStr(), dateStore.getEndDateStr());
  }

  if (state === 'loading') {
    return <Typography>Loading...</Typography>
  }
  else if (state === 'error') {
    return <Typography>Server unavailable</Typography>
  }
  else {
    let sum = 0;
    timeRecords.forEach(val => sum += val.duration);

    const workingDays = (dateRange == DateRange.Week ? 5 : (dateRange == DateRange.Month ? 21 : 1));
    const requiredTime = props.employee.workDayDuration * workingDays;

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
                <TableRow key={row.id || 0}>
                  <TableCell>
                    <SelectProject
                      projects={props.employee.projects}
                      value={row.projectId}
                      onChange={(e) => timeRecordsStore.handleSelectIdChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <SelectWorkType
                      value={row.workTypeId}
                      onChange={(e) => timeRecordsStore.handleSelectIdChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <input
                      name="date"
                      style={{maxWidth: 70}}
                      value={row.date}
                      size={10}
                      onChange={(e) => timeRecordsStore.handleInputChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <SelectTime
                      value={row.duration} 
                      onChange={(e) => timeRecordsStore.handleSelectChange(e, i)}
                    />
                  </TableCell>
                  <TableCell>
                    <input
                      name="description"
                      value={row.description}
                      size={36}
                      onChange={(e) => timeRecordsStore.handleInputChange(e, i)}
                    />
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => timeRecordsStore.saveRow(i)}>
                      <SaveIcon/>
                    </Button>
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => timeRecordsStore.deleteRow(i, row)}>
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
                    <Button onClick={() => timeRecordsStore.setEditingRow(i)}>
                      <EditIcon/>
                    </Button>
                  </TableCell>
                  <TableCell padding="none">
                    <Button onClick={() => timeRecordsStore.addRow(i)}>
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