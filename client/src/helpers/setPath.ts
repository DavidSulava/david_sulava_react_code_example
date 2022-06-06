const setPath = (path: string, parameter: (string|number|undefined)[] = []) => {
  const paramReg = new RegExp(':.*?(?=\\/)|:.*(?=$)');
  let newPath = path
  parameter.forEach(param => {
    newPath = newPath.replace(paramReg, param?.toString()||'')
  })
  return newPath
}

export default setPath