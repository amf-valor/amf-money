import { Moment } from 'moment';

export interface Token {
    hash: string
    expiryAt: Moment
    createdAt: Moment
}