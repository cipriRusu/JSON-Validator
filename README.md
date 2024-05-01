# JSON-Validator

The project represents a base for parsing a JSON file, making sure it respects the [JSON Schema](https://www.json.org/json-en.html). In it's current form it processes the file from the very beginning, processing it in a "Finite State Machine" manner. If at the end of the script all the file content was processed, it's considered the file adheres to the JSON schema so it returns a 0 (successful) result.
