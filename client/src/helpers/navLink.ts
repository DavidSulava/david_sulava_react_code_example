export const isNavLikActive = (route: string = "", location:string): boolean => {
  return location.endsWith(route)
}