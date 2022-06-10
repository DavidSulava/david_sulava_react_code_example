import React, { useRef, useState } from 'react';
import useClickOutside from '../../../helpers/hooks/useClickOutside';
import { useDispatch } from 'react-redux';
import { ACCESS_TOKEN_KEY, IUser } from '../../../types/user';
import { Button } from 'react-bootstrap';
import { setUser } from '../../../stores/authentication/reducer';
import { Nullable } from '../../../types/common';
import { setLocalStorage } from '../../../helpers/localStorage';
import BtnLink from '../../BtnLink';
import { ERoutes } from '../../../router/Routes';

const Profile: React.FC<{user:Nullable<IUser>}> = ({user }) => {
  const dispatch = useDispatch()
  const profileComponentRef = useRef<any>()
  const [isShowModal, setIsShowModal] = useState(false)

  useClickOutside(profileComponentRef, (ev)=>setIsShowModal(false))
  const onIconClick = () => {
    setIsShowModal(!isShowModal)
  }
  const onLogOut = () => {
    setLocalStorage(ACCESS_TOKEN_KEY, '')
    dispatch(setUser(null))
  }

  return(
    <div className="profile-wrapper" ref={profileComponentRef}>
      <div className="profile-icon" onClick={onIconClick}></div>
      {
        isShowModal &&
          <div className="profile-modal mt-1 me-1">
            <div> Hello {user?.firstName}, {user?.lastName}</div>
            <br/>
            <BtnLink to={ERoutes.ManageAccount} className='mb-1 btn btn-outline-primary' onClick={onIconClick}>Manage account</BtnLink>
            <Button variant="outlined" className="mb-1 btn-outline-primary" onClick={onLogOut}>
              Log Out
            </Button>
          </div>
      }
    </div>
  )
}
export default Profile