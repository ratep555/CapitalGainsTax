export interface IUser {
    displayName: string;
    email: string;
    token: string;
    roleName: string;
    role: Role;
  }

export enum Role {
    Admin = 'Admin'
  }


