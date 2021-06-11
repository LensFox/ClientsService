import { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { fetchClients } from './actions';
import ClientsTableContainer from './ClientsTableContainer';

function App() {
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(fetchClients())
  }, [dispatch])

  return <ClientsTableContainer />
}

export default App;
