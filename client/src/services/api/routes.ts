export const apiRoutes = {
  signIn: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Authentication`,
  signUp: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/User`,
  authOrg: (id: string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Authentication/organization?organizationId=${id}`,
  organisations: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization`,
    list: (id: string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization/byuser?userId=${id}`
  },
  tariff: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Tariff`
  },
  product: {
    root: (dataString: string = '') => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Product${dataString ? '?' + dataString : ''}`,
  },
  productVersion: {
    root: (dataString: string = '') => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/ProductVersion${dataString ? '?' + dataString : ''}`,
    item: (id: string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/ProductVersion/${id}`,
  },
  configurations: {
    root: (dataString: string = '') => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Configuration${dataString ? '?' + dataString : ''}`,
    params: (configurationId: string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Configuration/${configurationId}/parameters`,
  },
  appBundle: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/AppBundle`,
}