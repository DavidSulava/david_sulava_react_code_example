import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import { useEffect } from 'react';

//TODO: finish it
const Notifier = () => {
  const error = useSelector((state: IState) => state.common.error)
  const handledErrorCodes = [422]

  useEffect(()=>{
    console.log('error: ', error)
  },[error])

  useEffect(()=>{
    if(error?.message && handledErrorCodes.includes(error?.code))
      alert(error.message)
  },[error])

  return(
    <>
    </>
  )
}
export default Notifier