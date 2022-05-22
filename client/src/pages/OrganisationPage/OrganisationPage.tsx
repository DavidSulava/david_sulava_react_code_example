import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import { useDispatch, useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import React, { useEffect, useState } from 'react';
import { Button } from 'react-bootstrap';
import CreateNewOrganisationModal from './Modals/CreateNewOrganisationModal';
import { Link } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import { getOrganisations } from '../../stores/organisation/reducer';
import setPath from '../../helpers/setPath';
import BtnLink from '../../components/BtnLink';

const OrganisationPage = () => {
  const dispatch = useDispatch()
  const organisations = useSelector((store: IState) => store.organisation.organisations)
  const {user} = useAuthCheck()

  const [isShowCreateOrgModal, setShowCreateOrgModal] = useState(false)

  useEffect(() => {
    if(user)
      dispatch(getOrganisations(user?.id))
  }, [dispatch])

  const onCreateOrganisation = () => setShowCreateOrgModal(true)
  const onCreateOrganisationClose = () => setShowCreateOrgModal(false)

  const organisationsList = () => {
    return (
      <div className="organisations-list">
        <div className="org-list-header">
          <h6>
            Workspaces for {user?.email}
          </h6>
        </div>
        <div className="organisation-rows">
          {
            organisations.map((org, index) => {
              return (
                <div className="organisation-single-row" key={index}>
                  <div className='organisation-item-left'>
                    <h6>{org.name}</h6>
                    <div>{org.description}</div>
                  </div>
                  <div>
                    <BtnLink to={setPath(ERoutes.Dashboard, [org.id])} className='btn btn-primary'>Log into space</BtnLink>
                  </div>
                </div>
              )
            })
          }
        </div>
        <div className="org-list-footer">
          <div>
            See more
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className="organisations-page">
      <h3>Welcome back, {user?.firstName}</h3>
      <div className="org-list-wrapper mt-5">
        {
          organisations.length ? organisationsList() : <h6>No organisations are created for now</h6>
        }
      </div>
      <br/>
      {
        !!organisations.length &&
        <h6>Or Just</h6>
      }
      <div>
        <br/>
        <Button variant="primary" onClick={onCreateOrganisation}>
          Create new organisation
        </Button>
      </div>
      <CreateNewOrganisationModal onClose={onCreateOrganisationClose} isOpen={isShowCreateOrgModal}/>
    </div>
  )
}
export default OrganisationPage