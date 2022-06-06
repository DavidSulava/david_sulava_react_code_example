import { BASE_URL } from '../../constants/api';

export const apiRoutes = {
  signIn: `${BASE_URL}/Authentication`,
  signUp: `${BASE_URL}/User`,
  authOrg: (id: string) => `${BASE_URL}/Authentication/organization?organizationId=${id}`,
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
    params: (configurationId: string) => `${BASE_URL}/Configuration/${configurationId}/parameters`,
  },
  appBundle: `${BASE_URL}/AppBundle`,
}