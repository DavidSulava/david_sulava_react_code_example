import RouteList from './router/RouteList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { ERoutes } from './router/Routes';
import Layout from './components/Layout/Layout';
import './styles/base.scss';


function App() {
  return (
    <div className="App">
      <BrowserRouter>
        <Routes>
          <Route element={<Layout/>}>
            <Route path={`${ERoutes.Root}*`} element={<RouteList/>}/>
          </Route>
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
