import RouteList from './router/RouteList';
import { HashRouter, Route, Routes } from 'react-router-dom';
import { ERoutes } from './router/Routes';
import Layout from './components/Layout/Layout';
import './styles/base.scss';


function App() {
  return (
    <div className="App">
      <HashRouter>
        <Routes>
          <Route element={<Layout/>}>
            <Route path={`${ERoutes.Root}*`} element={<RouteList/>}/>
          </Route>
        </Routes>
      </HashRouter>
    </div>
  );
}

export default App;
