import ClientsTable from '../ClientsTable';

export default {
    clients: [],
    component: ClientsTable
};

const Template = (args) => <ClientsTable {...args} />;

export const EmptyList = Template.bind({});
EmptyList.args = {
    clients: [],
    onDelete: () => {},
    onSave: () => {}
};

export const FilledList = Template.bind({});
FilledList.args = {
    clients: [
        {
            id: 'Id 1',
            name: 'Element 1'
        },
        {
            id: 'Id 2',
            name: 'Element 2'
        },
        {
            id: 'Id 3',
            name: 'Element 3'
        },
        {
            id: 'Id 4',
            name: 'Element 4'
        }
    ],
    argTypes: {
        onDelete: { action: 'Deleted' },
        onSave: { action: 'Saved' }
    }
};
