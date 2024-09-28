import { eSystemRole } from '../_generated/enums';

export interface ICurrentUser {
  id?: string;
  username?: string;
  email?: string;
  roles?: eSystemRole[];
  token?: string;
  tokenExpiryDate?: Date;
}
