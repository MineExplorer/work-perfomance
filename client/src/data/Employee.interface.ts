export enum Seniority {
  NotAvailable,
  Junior,
  JuniorMiddle,
  Middle,
  MiddleSenior,
  Senior
};

export enum PermissionLevel
{
  Default,
  Manager
}

export interface Employee {
  id: number;
  fullName: string;
  email: string;
  password: string;
  permissionLevel: PermissionLevel;
  seniority: Seniority;
  experience: number;
  hourlyRate: number;
  created: string;
  workDayDuration: number;
  projects?: Project[];
}

export interface Project {
  id: number;
  title: string;
  archived: boolean;
  employees?: Employee[];
}

