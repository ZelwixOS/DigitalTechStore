import ParameterValue from './ParameterValue';

type Parameter = {
  id: string;
  name: string;
  value: string;
  important: boolean;
  range: boolean;
  minValue: number;
  maxValue: number;
  parameterValues: ParameterValue[];
};

export default Parameter;
