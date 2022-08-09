import { addDays } from 'date-fns';
import {action, makeObservable, observable} from 'mobx';
import { createContext } from 'react';
import { DateRange } from '../components/Select/SelectDateRange';
import { Employee, TimeInterval } from '../data';
import { deleteFunctionApi, fetchFunctionApi, postFunctionApi, putFunctionApi } from '../helpers';

export class TimeTableStore {
  @observable
  timeRecords: TimeInterval[];

  @observable
  editingRow: number = -1;

  @observable
  state: 'loading' | 'loaded' | 'error' = 'loading';

  @observable
  employee: Employee;

  constructor() {
		makeObservable(this);
	}

  @action
  setEmployee(employee: Employee) {
    this.employee = employee;
  }

  @action
  async fetchTimeIntervals(startDate: string, endDate: string) {
    try {
			this.timeRecords = await fetchFunctionApi<TimeInterval[]>(`/TimeInterval/?employeeId=${this.employee.id}&dateStart=${startDate}&dateEnd=${endDate}`);
		} catch (error) {
			this.state = 'error';
			console.log(error);
			return;
		}

    this.state = 'loaded';
  }

  @action
  addRow(index: number = this.timeRecords.length - 1) {
    if (this.editingRow != -1) return;

    const project = this.employee.projects[0];
    this.timeRecords.splice(index + 1, 0, {
      employeeId: this.employee.id,
      projectId: project.id,
      project: project.title,
      workTypeId: 1,
      workType: "Разработка",
      date: "",
      duration: 0.25,
      description: ""
    });
    this.editingRow = index + 1;
  }

  @action
  setEditingRow(index: number) {
    this.editingRow = index;
  }

  @action
  async deleteRow(i: number, row: TimeInterval) {
    this.timeRecords.splice(i, 1);
    this.editingRow = -1;
    if (row.id) {
      await deleteFunctionApi(`/TimeInterval/${row.id}`);
    }
  }

  @action
  async saveRow(i: number) {
    this.setEditingRow(-1);
    const row = this.timeRecords[i];
    if (!row.id) {
      const newRow = await postFunctionApi<TimeInterval>(`/TimeInterval/`, JSON.stringify(row));
      row.id = newRow.id;
    } else {
      await putFunctionApi(`/TimeInterval/${row.id}`, JSON.stringify(row));
    }
  }

  @action 
  handleInputChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
    const { name, value } = e.target;
    this.timeRecords[index][name] = value;
  };

  @action
  handleSelectIdChange(e: React.ChangeEvent<HTMLSelectElement>, index: number) {
    const name = e.target.name;
    const value = parseInt(e.target.value);
    const text = e.target.options[value - 1].text;
    this.timeRecords[index][name + "Id"] = value;
    this.timeRecords[index][name] = text;
  };

  @action
  handleSelectChange(e: React.ChangeEvent<HTMLSelectElement>, index: number) {
    const name = e.target.name;
    this.timeRecords[index][name] = parseFloat(e.target.value);
  };
}

export const TimeTableContext = createContext<TimeTableStore>(null);