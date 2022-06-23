import { Outlet } from "react-router-dom"
import Header from '../Header/Header';
import { useSelector } from 'react-redux';
import { IState } from '../../stores/configureStore';
import { useEffect } from 'react';
import Notifier from '../Notifier/Notifier';

const Layout = () => {
  return (
    <>
      <Header/>
      <Outlet/>
      <Notifier/>
    </>
  )
}

export default Layout