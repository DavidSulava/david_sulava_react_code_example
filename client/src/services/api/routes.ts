export const apiRoutes = {
  signIn: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Authentication`,
  signUp: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/User`,
  organisations: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization`,
    list: (id:string) => `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Organization/byuser?userId=${id}`
  },
  tariff: {
    root: `${process.env.REACT_APP_REMOTE_SERVER_BASE_URL}/Tariff`
  }
}