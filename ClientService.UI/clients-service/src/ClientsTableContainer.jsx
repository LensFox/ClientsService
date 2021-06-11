import React from 'react';
import {useDispatch, useSelector} from 'react-redux';
import { addClient, deleteClient } from './actions';
import ClientsTable from './ClientsTable';

const ClientsTableContainer = () => {
    const dispatch = useDispatch();

    const clients = useSelector(state => state.clients);

    const onDelete = (id) => dispatch(deleteClient(id));

    const onSave = (client) => dispatch(addClient(client));

    return (<ClientsTable clients={clients} onDelete={onDelete} onSave={onSave} />);
}

export default ClientsTableContainer;