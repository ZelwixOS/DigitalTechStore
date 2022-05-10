import React, { JSXElementConstructor, ReactElement } from 'react';
import { Dialog, DialogContent, DialogTitle } from '@material-ui/core';

interface IModalFormDialog {
  name: string;
  open: boolean;
  form: ReactElement<unknown, string | JSXElementConstructor<unknown>>;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const ModalFormDialog: React.FC<IModalFormDialog> = props => {
  const handleClose = () => {
    props.setOpen(false);
  };

  return (
    <Dialog
      open={props.open}
      onClose={handleClose}
      aria-labelledby="create-dialog-title"
      aria-describedby="create-dialog-description"
    >
      <DialogTitle id="create-dialog-title">{props.name}</DialogTitle>
      <DialogContent>{props.form}</DialogContent>
    </Dialog>
  );
};

export default ModalFormDialog;
