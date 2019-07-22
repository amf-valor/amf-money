import { Moment } from 'moment';

export interface Account {
    birth: Moment
    email: string
    password: string
    pin: string 
}