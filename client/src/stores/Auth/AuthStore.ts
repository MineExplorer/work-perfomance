import { action, makeObservable, observable } from 'mobx';
import { Employee } from '../../data';
import { fetchFunctionApi } from '../../helpers';

export class AuthStore {
	@observable
  	public state: 'loading' | 'loaded' | 'error' = 'loading';

	@observable
	public isUserAuthenticated = false;

	@observable
	public isUserAuthorized = false;

	@observable
	public userData: Employee;

	constructor() {
		makeObservable(this);
		this.fetchUserInfo();
	}

	@action
	private async fetchUserInfo() {
		const employeeId = 1;

		try {
			this.userData = await fetchFunctionApi<Employee>(`/Employee/${employeeId}`);
		} catch (error) {
			this.state = 'error';
			console.log(this.state);
			return;
		}

		if (!this.userData) {
			this.state = 'error';
			return;
		}

		this.isUserAuthenticated = true;
		this.isUserAuthorized = true;
		this.state = 'loaded';
		console.log(this.state);
	}
}