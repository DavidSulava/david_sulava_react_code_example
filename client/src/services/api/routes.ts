import { BASE_URL } from '../../constants/api';

export const apiRoutes = {
  signUp: `${BASE_URL}/User`,
  account: `${BASE_URL}/Account`,
  auth:{
    signIn: `${BASE_URL}/Authentication`,
    org: (id: string) => `${BASE_URL}/Authentication/organization?organizationId=${id}`,
    pwdEmail: `${BASE_URL}/Authentication/PasswordRecovery`,
    pwdConfirm: `${BASE_URL}/Authentication/PasswordRecoveryConfirm`,
  },
  organisations: {
    root: `${BASE_URL}/Organization`,
    list: (id: string) => `${BASE_URL}/Organization/byuser?userId=${id}`
  },
  tariff: {
    root: `${BASE_URL}/Tariff`
  },
  product: {
    root: (dataString: string = '') => `${BASE_URL}/Product${dataString ? '?' + dataString : ''}`,
  },
  productVersion: {
    root: (dataString: string = '') => `${BASE_URL}/ProductVersion${dataString ? '?' + dataString : ''}`,
    item: (id: string) => `${BASE_URL}/ProductVersion/${id}`,
  },
  configurations: {
    root: (dataString: string = '') => `${BASE_URL}/Configuration${dataString ? '?' + dataString : ''}`,
    item: (id: string) => `${BASE_URL}/Configuration/${id}`,
    params: (configurationId: string) => `${BASE_URL}/Configuration/${configurationId}/parameters`,
    svf: (id: string, svfName?: string) => `${BASE_URL}/Configuration/${id}/svf${svfName? '/'+svfName : ''}`,
  },
  appBundle: {
    root: (dataString: string = '')=>`${BASE_URL}/AppBundle${dataString ? '?' + dataString : ''}`,
    item: (id:string) => `${BASE_URL}/AppBundle/${id}`
  },
}