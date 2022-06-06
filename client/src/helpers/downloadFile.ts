const downloadFile = (data:Blob, file:string)=>{
  const url = URL.createObjectURL( new Blob([data]) )
  const link = document.createElement('a')

  link.href = url
  link.setAttribute('download', file)
  document.body.appendChild(link)
  link.click()
  link.remove()
}

export default downloadFile