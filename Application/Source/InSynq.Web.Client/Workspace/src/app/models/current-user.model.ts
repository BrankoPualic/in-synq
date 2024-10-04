import { eSystemRole } from '../_generated/enums';

export interface ICurrentUser {
  id?: number;
  username?: string;
  email?: string;
  roles?: eSystemRole[];
  token?: string;
  tokenExpirationDate?: Date;
}
