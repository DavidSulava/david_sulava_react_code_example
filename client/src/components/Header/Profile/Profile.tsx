import React, { useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import useClickOutside from '../../../helpers/hooks/useClickOutside';
import { ERoutes } from '../../../router/Routes';
import { useDispatch } from 'react-redux';
import { AuthenticationActions } from '../../../stores/authentication/actions';
import { IUser, Nullable } from '../../../types/user';
import { Button } from 'react-bootstrap';

const Profile: React.FC<{user:Nullable<IUser>}> = ({user }) => {
  const dispatch = useDispatch()
  const profileComponentRef = useRef<any>()
  const [isShowModal, setIsShowModal] = useState(false)

  useClickOutside(profileComponentRef, (ev)=>setIsShowModal(false))
  const onIconClick = () => {
    setIsShowModal(!isShowModal)
  }
  const onLogOut = () => {
    dispatch(AuthenticationActions.setUser(null))
  }

  return(
    <div className="profile-wrapper" ref={profileComponentRef}>
      <div className="profile-icon" onClick={onIconClick}></div>
      {
        isShowModal &&
          <div className="profile-modal mt-1 me-1">
            <div> Hello {user?.firstName}, {user?.lastName}</div>
            <br/>
            <Button variant="outlined" className="mb-1 btn-outline-primary">
              Manage account
            </Button>
            <Button variant="outlined" className="mb-1 btn-outline-primary" onClick={onLogOut}>
              Log Out
            </Button>
          </div>
      }
    </div>
  )
}
export default Profile