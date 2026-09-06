# Estrelio.AdditionalFluentValidation

Contains additional FluentValidation rules

## Phone number rules

`PhoneNumber()` uses libphonenumber's strict metadata-range validation. Use it when an accepted number must match a documented allocation range.

`PossiblePhoneNumber()` accepts only complete structurally possible phone numbers. The overload accepting a selector supports national-format input by providing the request's ISO alpha-2 parsing region, such as `MY`. The selector must return a region code, not a calling code such as `+60`.
