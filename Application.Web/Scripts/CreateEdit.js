var urlContact = "/contact";
var AddressTypeData;
var PhoneTypeData;
var url = window.location.pathname;
var profileId = url.substring(url.lastIndexOf('/') + 1);

$.ajax({
    url: urlContact + '/InitializePageData',
    async: false,
    dataType: 'json',
    success: function (json) {
        AddressTypeData = json.lstAddressTypeDTO;
        PhoneTypeData = json.lstPhoneTypeDTO;
    }
});

$(function () {

var Profile = function (profile) {
    var self = this;
    self.ProfileId = ko.observable(profile ? profile.ProfileId : 0).extend({ required: true });
    self.FirstName = ko.observable(profile ? profile.FirstName : '').extend({ required: true, maxLength: 50 });
    self.LastName = ko.observable(profile ? profile.LastName : '').extend({ required: true, maxLength: 50 });
    self.Email = ko.observable(profile ? profile.Email : '').extend({ required: true, maxLength: 50, email: true });
    self.PhoneDTO = ko.observableArray(profile ? profile.PhoneDTO : []);
    self.AddressDTO = ko.observableArray(profile ? profile.AddressDTO : []);
};

var PhoneLine = function (phone) {
    var self = this;
    self.PhoneId = ko.observable(phone ? phone.PhoneId : 0);
    self.PhoneTypeId = ko.observable(phone ? phone.PhoneTypeId : undefined).extend({ required: true });
    self.Number = ko.observable(phone ? phone.Number : '').extend({ required: true, maxLength: 25, phoneUS: true });
};

var AddressLine = function (address) {
    var self = this;
    self.AddressId = ko.observable(address ? address.AddressId : 0);
    self.AddressTypeId = ko.observable(address ? address.AddressTypeId : undefined).extend({ required: true });
    self.AddressLine1 = ko.observable(address ? address.AddressLine1 : '').extend({ required: true, maxLength: 100 });
    self.AddressLine2 = ko.observable(address ? address.AddressLine2 : '').extend({ required: true, maxLength: 100 });
    self.Country = ko.observable(address ? address.Country : '').extend({ required: true, maxLength: 50 });
    self.State = ko.observable(address ? address.State : '').extend({ required: true, maxLength: 50 });
    self.City = ko.observable(address ? address.City : '').extend({ required: true, maxLength: 50 });
    self.ZipCode = ko.observable(address ? address.ZipCode : '').extend({ required: true, maxLength: 15 });
};


var ProfileCollection = function () {
    var self = this;

    //if ProfileId is 0, It means Create new Profile
    if (profileId == 0) {
        self.profile = ko.observable(new Profile());
        self.phoneNumbers = ko.observableArray([new PhoneLine()]);
        self.addresses = ko.observableArray([new AddressLine()]);
    }
    else {
        $.ajax({
            url: urlContact + '/GetProfileById/' + profileId,
            async: false,
            dataType: 'json',
            success: function (json) {
                self.profile = ko.observable(new Profile(json));
                self.phoneNumbers = ko.observableArray(ko.utils.arrayMap(json.PhoneDTO, function (phone) {
                    return phone;
                }));
                self.addresses = ko.observableArray(ko.utils.arrayMap(json.AddressDTO, function (address) {
                    return address;
                }));
            }
        });
    }

    self.addPhone = function () { self.phoneNumbers.push(new PhoneLine()) };

    self.removePhone = function (phone) { self.phoneNumbers.remove(phone) };

    self.addAddress = function () { self.addresses.push(new AddressLine()) };

    self.removeAddress = function (address) { self.addresses.remove(address) };

    self.backToProfileList = function () { window.location.href = '/contact'; };

    self.profileErrors = ko.validation.group(self.profile());
    self.phoneErrors = ko.validation.group(self.phoneNumbers(), { deep: true });
    self.addressErrors = ko.validation.group(self.addresses(), { deep: true });

    self.saveProfile = function () {

        var isValid = true;

        if (self.profileErrors().length != 0) {
            self.profileErrors.showAllMessages();
            isValid = false;
        }

        if (self.phoneErrors().length != 0) {
            self.phoneErrors.showAllMessages();
            isValid = false;
        }

        if (self.addressErrors().length != 0) {
            self.addressErrors.showAllMessages();
            isValid = false;
        }

        if( isValid)
        {
            self.profile().AddressDTO = self.addresses;
            self.profile().PhoneDTO = self.phoneNumbers;

            $.ajax({
                type: (self.profile().ProfileId > 0 ? 'PUT' : 'POST'),
                cache: false,
                dataType: 'json',
                url: urlContact + (self.profile().ProfileId > 0 ? '/UpdateProfileInformation?id=' + self.profile().ProfileId : '/SaveProfileInformation'),
                data: JSON.stringify(ko.toJS(self.profile())), 
                contentType: 'application/json; charset=utf-8',
                async: false,
                success: function (data) {
                    window.location.href = '/contact';
                },
                error: function (err) {
                    var err = JSON.parse(err.responseText);
                    var errors = "";
                    for (var key in err) {
                        if (err.hasOwnProperty(key)) {
                            errors += key.replace("profile.", "") + " : " + err[key];
                        }
                    }
                    $("<div></div>").html(errors).dialog({ modal: true, title: JSON.parse(err.responseText).Message, buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                },
                complete: function () {
                }
            });
        }
    };
};

ko.applyBindings(new ProfileCollection());
});

var clone = (function () {
    return function (obj) {
        Clone.prototype = obj;
        return new Clone()
    };
    function Clone() { }
}());