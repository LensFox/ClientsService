import React, { useState } from 'react';
import Table from '@material-ui/core/Table';
import TableCell from '@material-ui/core/TableCell';
import TableRow from '@material-ui/core/TableRow';
import TableHead from '@material-ui/core/TableHead';
import TableBody from '@material-ui/core/TableBody';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import TextField from '@material-ui/core/TextField';
import Button from '@material-ui/core/Button';
import { uuidv4 } from './helpers';

const ClientsTable = ({ clients, onDelete, onSave }) => {
    const [newClientName, setNewClientName] = useState('');

    const handleOnNameChange = (event) => {
        setNewClientName(event.target.value)
    }

    const handleOnAddClient = () => {
        const client = {
            id: uuidv4(),
            name: newClientName
        };
        onSave(client);
    }

    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell>Id</TableCell>
                    <TableCell>Name</TableCell>
                    <TableCell />
                </TableRow>
            </TableHead>
            <TableBody>
                <TableRow>
                    <TableCell />
                    <TableCell><TextField value={newClientName} onChange={handleOnNameChange}/></TableCell>
                    <TableCell><Button onClick={handleOnAddClient} variant={'outlined'}>Save</Button></TableCell>
                </TableRow>
                {clients.map(client => (
                    <TableRow key={client.id}>
                        <TableCell>{client.id}</TableCell>
                        <TableCell>{client.name}</TableCell>
                        <TableCell>
                            <IconButton onClick={() => onDelete(client.id)}>
                                <DeleteIcon />
                            </IconButton>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    )
}

export default ClientsTable