import { ref, reactive, computed } from "@vue/composition-api";

export const validatedRef = ({ value, rules = {}, context = {} }) => {
  const initialValue = value;
  const validatorsEntries = Object.entries(rules);
  const validatorsResultsRef = ref(
    validatorsEntries.map(([name]) => [name, null])
  );
  const valueRef = ref(value);
  const touchedRef = ref(false);
  const errorRef = ref(null);
  const errorMessagesRef = ref([]);

  const validate = () => {
    touchedRef.value = true;
    const resultEntries = validatorsEntries.map(([name, validator]) => [
      name,
      validator(valueRef.value, context),
    ]);

    const invalidResults = resultEntries
      .map((entry) => entry[1])
      .filter((result) => result === false || result.valid === false);

    errorRef.value = invalidResults.length > 0;

    errorMessagesRef.value = invalidResults
      .filter((result) => result.message)
      .map((result) => result.message);

    validatorsResultsRef.value = resultEntries;
  };

  const reset = () => {
    valueRef.value = initialValue;
    touchedRef.value = false;
  };

  return {
    get value() {
      return valueRef.value;
    },
    set value(value) {
      valueRef.value = value;
    },
    get rules() {
      return Object.fromEntries(validatorsResultsRef.value);
    },
    get errorMessages() {
      return errorMessagesRef.value;
    },
    get isTouched() {
      return touchedRef.value;
    },
    get isDirty() {
      return initialValue !== valueRef.value;
    },
    get isError() {
      return !!errorRef.value;
    },
    get isValid() {
      return touchedRef.value ? touchedRef.value && !errorRef.value : null;
    },
    validate,
    reset,
  };
};

export const useForm = ({ onSubmit = () => {}, context = {} } = {}) => {
  const form = reactive(new Map());

  const useField = (name, { value = null, rules = {} } = {}) => {
    form.set(name, validatedRef({ value, rules, context }));
  };

  const validate = () => {
    form.forEach((field) => field.validate());
  };

  const reset = () => {
    form.forEach((field) => field.reset());
  };

  const isValid = computed(() =>
    [...form].map((field) => field[1].isValid).reduce((acc, val) => acc && val)
  );

  const submit = (context) => {
    validate();
    if (isValid.value) {
      onSubmit(context);
    }
  };

  const fields = computed({
    get: () =>
      Object.fromEntries(
        [...form].map(([name, validatedRef]) => [name, validatedRef.value])
      ),
    set: (val) => {
      const entries = Object.entries(val);
      entries.forEach(([name, value]) => {
        if (form.has(name)) {
          form.get(name).value = value;
          return;
        }
        useField(name, { value });
      });
    },
  });

  const formObject = computed({
    get: () => Object.fromEntries(form),
    set: (val) => {
      const entries = Object.entries(val);
      entries.forEach(([name, value]) => {
        if (form.has(name)) {
          form.get(name).value = value;
          return;
        }
        useField(name, { value });
      });
    },
  });

  return {
    form: formObject,
    fields,
    useField,
    validate,
    isValid,
    submit,
    reset,
  };
};
