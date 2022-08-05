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
    seniority: Seniority;
    experience: number;
    techStack: string;
    created: string;
    projects: Project[];
}

export interface Project {
    id: number;
    title: string;
    archived: boolean;
    employees?: Employee[];
}

