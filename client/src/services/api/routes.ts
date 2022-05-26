export const apiRoutes = {
  signIn: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Authentication`,
  signUp: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/User`,
  authOrg: (id:string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Authentication/organization?organizationId=${id}`,
  organisations: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization`,
    list: (id: string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization/byuser?userId=${id}`
  },
  tariff: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Tariff`
  },
  product: {
    root: (dataString: string ='') => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Product?${dataString}`,
  },
  productVersion: {
    root: (dataString: string ='') => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/ProductVersion?${dataString}`,
  },
  appBundle: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/AppBundle`,
}